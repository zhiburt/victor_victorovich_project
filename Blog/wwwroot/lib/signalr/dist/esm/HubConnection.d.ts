import { IStreamResult } from "./Stream";
export declare class HubConnection {
    private readonly connection;
    private readonly logger;
    private protocol;
    private handshakeProtocol;
    private callbacks;
    private methods;
    private id;
    private closedCallbacks;
    private timeoutHandle;
    private receivedHandshakeResponse;
    serverTimeoutInMilliseconds: number;
    private constructor();
    start(): Promise<void>;
    stop(): Promise<void>;
    stream<T = any>(methodName: string, ...args: any[]): IStreamResult<T>;
    send(methodName: string, ...args: any[]): Promise<void>;
    invoke<T = any>(methodName: string, ...args: any[]): Promise<T>;
    on(methodName: string, newMethod: (...args: any[]) => void): void;
    off(methodName: string, method?: (...args: any[]) => void): void;
    onclose(callback: (error?: Error) => void): void;
    private processIncomingData(data);
    private processHandshakeResponse(data);
    private configureTimeout();
    private serverTimeout();
    private invokeClientMethod(invocationMessage);
    private connectionClosed(error?);
    private cleanupTimeout();
    private createInvocation(methodName, args, nonblocking);
    private createStreamInvocation(methodName, args);
    private createCancelInvocation(id);
}
