import { Component } from "@angular/core";
import { PlayerCredentialsDto } from "../data/dto/player-credentials-dto";
import { HttpService } from "../http/http.service";
import { ApiCallResponse } from "../data/response/api-call-response";
import { Router } from "@angular/router";

@Component({
    templateUrl: "./register.component.html",
    styleUrls: ["./register.component.css"]
})
export class RegisterComponent {

    confirmPasswordLabel: string = 'Confirm Password: ';
    errorMessage: string = '';

    username: string = '';
    password: string = '';
    confirmPass: string = '';

    buttonLocked: boolean = false;

    constructor(private apiConnectionService: HttpService, private router: Router) { }

    onCreateAccount(): void {
        this.buttonLocked = true;
        let passesMatch: boolean = this.password == this.confirmPass;

        if (!passesMatch) {
            this.errorMessage = 'Passwords must match!';
            console.log("Passwords don't match");
            this.buttonLocked = false;
            return;
        }
        else {
            let newPlayerCredentialsDto = new PlayerCredentialsDto(`${this.username}`, `${this.password}`);
            console.log("Registering new player...");
            this.apiConnectionService.makeRequestWithBody<ApiCallResponse>('Players/register', 'POST', newPlayerCredentialsDto)
                .subscribe({
                    next: (registerResponse) => {
                        console.log("Registration successful!");

                        this.router.navigate(['signin'], { queryParams: { username: this.username, password: this.password } });
                    },
                    error: (registerError) => {
                        console.log("Error in registration: ", registerError);
                        if (!registerError.errorMessage) {
                            this.errorMessage = "Unable to register. Please try again in a few moments.";
                        }
                        else {
                            this.errorMessage = registerError.errorMessage;
                        }
                        this.buttonLocked = false;
                    }
                }
                );
        }
    }
}
