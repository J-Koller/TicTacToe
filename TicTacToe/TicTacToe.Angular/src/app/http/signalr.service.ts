import { EventEmitter, Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Observable, forkJoin, from } from 'rxjs';
import { Hubs } from '../data/enums/hubs';
import { PlayerDto } from '../data/dto/player-dto';

@Injectable({
    providedIn: 'root'
})
export class SignalRService {
    private _gameConnection!: HubConnection;
    private _chatConnection!: HubConnection;

    private _domain = 'http://localhost:5159';

    private _chatHubEndPoint = 'chatHub';
    private _gameHubEndPoint = 'gameHub';

    public userGameConnectionId: string = '';
    public opponentGameConnectionId: string = '';
    public chatConnectionId: string = '';

    public send<T>(hub: Hubs, methodName: string, argument: T, ...connectionIds: string[]): void {
        try {
            if (hub === Hubs.Game) {
                this._gameConnection.invoke(methodName, argument, connectionIds);
            } else if (hub === Hubs.Chat) {
                this._chatConnection.invoke(methodName, argument);
            }
        } catch (error) {
            console.error(error);
        }
    }

    public configureOn<TArg>(hub: Hubs, methodName: string, handler: (arg: TArg) => void): void {
        if (hub === Hubs.Game) {
            this._gameConnection.on(methodName, handler);
        } else if (hub === Hubs.Chat) {
            this._chatConnection.on(methodName, handler);
        }
    }

    public configureOnWithReturn<TArg, TReturn>(
        hub: Hubs,
        methodName: string,
        handler: (arg: TArg) => TReturn
    ): void {
        if (hub === Hubs.Game) {
            this._gameConnection.on(methodName, handler);
        } else if (hub === Hubs.Chat) {
            this._chatConnection.on(methodName, handler);
        }
    }

    public stopConnections(): Promise<void[]> {
        if(this._gameConnection == undefined || this._gameConnection == null){
            return Promise.resolve([]);
        }
        const gameConnection$ = this._gameConnection.stop();
        const chatConnection$ = this._chatConnection.stop();

        return Promise.all([gameConnection$, chatConnection$]);
    }

    public startConnections(): void {
        this._gameConnection = new HubConnectionBuilder()
            .withUrl(`${this._domain}/${this._gameHubEndPoint}`)
            .withAutomaticReconnect()
            .build();

        // this.userGameConnectionId = this._gameConnection.connectionId!;

        this._gameConnection.start()
            .then(() => {
                console.log('Connection started: ' + this._gameConnection.connectionId);
                this.userGameConnectionId = this._gameConnection.connectionId!;
            })
            .catch(err => { console.log('Error while starting connection: ' + err + `: ${this._gameConnection.connectionId}`) });

        this._chatConnection = new HubConnectionBuilder()
            .withUrl(`${this._domain}/${this._chatHubEndPoint}`)
            .withAutomaticReconnect()
            .build();
    }
}