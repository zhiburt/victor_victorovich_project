using Blog;
using Blog.Controllers;
using Blog.Db.Controllers;
using Blog.Models.DbModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub 
    {
        private readonly MessageBoxContext _messageBoxContext;
        private readonly MessageBoxLayerContext _messageBoxLayerContext;
        private readonly MessageContext _messageContext;
        private readonly UserManager<UserIndentity> _userManager;
        private readonly SignInManager<UserIndentity> _signInManager;
        private readonly ILogger<AutheficationController> _logger;

        public ChatHub() : base()
        {
            this._messageBoxContext = 
                (MessageBoxContext)Startup.
                __serviceProvider.GetRequiredService(typeof (MessageBoxContext));
            this._messageBoxLayerContext =
                (MessageBoxLayerContext)Startup.
                __serviceProvider.GetRequiredService(typeof (MessageBoxLayerContext));
            this._messageContext = 
                (MessageContext)Startup.
                __serviceProvider.GetRequiredService(typeof (MessageContext));
            this._userManager =
                (UserManager<UserIndentity>)Startup.
                __serviceProvider.GetRequiredService(typeof (UserManager<UserIndentity>));
            this._signInManager = 
                (SignInManager<UserIndentity>)Startup.
                __serviceProvider.GetRequiredService(typeof (SignInManager<UserIndentity>));
            this._logger = 
                (ILogger<AutheficationController>)Startup.
                __serviceProvider.GetRequiredService(typeof (ILogger<AutheficationController>));
        }

        public const string SEPARATOR = "|";        
        
        // public async Task FirstUpdateMessages(){

        // }
        public async Task SendMessage(string message)
        {
            var user = await _userManager.GetUserAsync(Context.User);
            await Clients.All.SendAsync("ReceiveMessage",user.UserName, message);
        }

        public async Task SendGroupMessage(string group, string message)
        {

            await Clients.All.SendAsync("ReceiveGroupMessage", group, message);
        }

        //refactoring
        public async Task JoinRoom(string roomName)
        {
            var user = await _userManager.GetUserAsync(Context.User);
            if(user != null){
                if(user.MessagesBoxLayerId == null){
                    var messageBoxLayer = new MessagesBoxLayer();
                    _messageBoxLayerContext.Comments.Add(messageBoxLayer);
                    await _messageBoxLayerContext.SaveChangesAsync();
                    
                    user.MessagesBoxLayerId = messageBoxLayer.Id;
                    await _userManager.UpdateAsync(user);
                }

                var layer = await _messageBoxLayerContext.Comments.FirstOrDefaultAsync(n => n.Id == user.MessagesBoxLayerId);
                var room = await _messageBoxContext.MessageBoxs.FirstOrDefaultAsync(n => n.Name == roomName);
                
                if(layer != null){
                    if(room == null){
                        room = new MessageBox{
                                Name = roomName,
                                CreatorId = user.Id,
                                TimeCreated = DateTime.UtcNow,
                                MessageBoxLevel = MessageBoxLevel.Simple
                            };
                        _messageBoxContext.MessageBoxs.Add(room);
                        await _messageBoxContext.SaveChangesAsync();
                
                    }

                    var otherUsers = SplitField(room.OtherUsersId, SEPARATOR);
                    bool checkValidUser = otherUsers?.Contains(user.Id) ?? false;                                 
                    if(
                        room.CreatorId != user.Id && !checkValidUser
                    ){
                        room.OtherUsersId = AddNewInfo(room.OtherUsersId, user.Id, SEPARATOR);    
                        await _messageBoxContext.SaveChangesAsync();
                    }
                    otherUsers = SplitField(room.OtherUsersId, SEPARATOR);
                    bool isUserInRoom = otherUsers?.Contains(user.Id) ?? false;
                    if(isUserInRoom || room.CreatorId == user.Id){        

                        layer.MessageBoxesId = AddNewInfo(layer.MessageBoxesId, room.Id.ToString(), SEPARATOR);
                        await _messageBoxLayerContext.SaveChangesAsync();
                        
                        await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
                        await Clients.Group(roomName).SendAsync($"Joined", user.UserName);
                    }
                    // var enumerableBoxesId = SplitField(layer.MessageBoxesId, SEPARATOR);
                    // var boxes = GetElements(enumerableBoxesId, _messageBoxContext.MessageBoxs);
                }
            }
            
            //+ message DB!!!
        }
        public async Task LeaveRoom(string roomName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);            
            await Clients.Group(roomName).SendAsync($"{Context.User.Identity.Name} leave");
        }

        #region private

        private async Task<IEnumerable<T>> GetElements<T>(IEnumerable<String> elementsId, IEnumerable<T> context) where T : class
        {
            return await Task.Run(() =>
            {
                if(elementsId == null)
                    return null;
            

                var responceElements = new List<T>();

                T element = null;
                foreach (var strId in elementsId)
                {
                    element = context.FirstOrDefault(t => (string)(t.GetType().GetProperty("Id").GetValue(t)) == strId);
                    if (element != null)
                    {
                        responceElements.Add(element);
                    }

                    element = null;
                }

                return responceElements.Count == 0 ? null : responceElements;

            });
        }

        public IEnumerable<string> SplitField(string field, string separator)
        {
            return field?.Split(separator) ?? null;
        }

        public string AddNewInfo(string field, string newField, string separator)
        {
            if (field == null)
            {
                return newField;
            }

            return (field + separator + newField);
        }
        
        #endregion
    }
}