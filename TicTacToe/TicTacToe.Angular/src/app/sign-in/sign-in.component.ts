import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { ApiCallDataResponse } from "../data/response/api-call-data-response";
import { ApiCallResponse } from "../data/response/api-call-response";
import { HttpService } from "../http/http.service";
import { SignInService } from "../services/sign-in.service";
import { SignalRService } from "../http/signalr.service";

@Component({
    selector: "app-sign-in",
    templateUrl: "./sign-in.component.html",
    styleUrls: ["./sign-in.component.css"]
})
export class SignInComponent implements OnInit {

    username: string = "";
    password: string = "";

    errorMessage: string = '';

    buttonLocked: boolean = false;


    constructor(private apiConnectionService: HttpService,
        private notificationService: SignalRService,
        private router: Router,
        private route: ActivatedRoute,
        private signInService: SignInService) { }


    ngOnInit(): void {
        this.route.queryParams.subscribe(params => {
            this.username = params["username"] ?? '';
            this.password = params["password"] ?? '';
        });

        this.notificationService.stopConnections();
    }

    onSignIn(): void {
        this.buttonLocked = true;
        console.log(this.username);
        console.log(this.password);
        if (this.username.trim() === "" || this.password.trim() === "") {
            this.errorMessage = "Please enter a username and a password.";
            this.buttonLocked = false;
            return;
        }
        const playerCredentialsDto = {
            username: this.username,
            password: this.password
        };

        this.apiConnectionService.makeRequestWithBody<ApiCallDataResponse<number>>('Players/signin', 'POST', playerCredentialsDto)
            .subscribe({
                next: signInResponse => {
                    console.log("signed in!");
                    console.log("Sending heartbeat..");
                    this.apiConnectionService.makeRequestWithoutBody<ApiCallResponse>(`Players/heartbeat/${signInResponse.dto}`, 'POST')
                        .subscribe({
                            next: heartBeatResponse => {
                                console.log("Heartbeat successful!");
                                console.log("Routing to main view...");

                                this.signInService.playerId = signInResponse.dto;
                                this.router.navigate(['main']);
                            }
                        });
                },
                error: (error: ApiCallDataResponse<number>) => {
                    if (!error.errorMessage) {
                        this.errorMessage = "Unable to sign in. Please try again in a few moments.";
                    }
                    else {
                        this.errorMessage = error.errorMessage;
                    }
                    this.buttonLocked = false;
                }
            });

    }

    onRegister(): void {
        this.buttonLocked = true;
        this.router.navigate(['register']);
    }

    onDownload(): void {
        // Once upon a time, a download link lived here. Removed due to this being a public repo
    }
}
