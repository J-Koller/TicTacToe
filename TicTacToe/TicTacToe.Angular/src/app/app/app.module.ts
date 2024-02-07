import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MenuComponent } from '../menu/menu.component';
import { GameBoardComponent } from '../game-board/game-board.component';
import { MainComponent } from '../main/main.component';
import { SignInComponent } from '../sign-in/sign-in.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { RegisterComponent } from '../register/register.component';

@NgModule({
  declarations: [
    AppComponent,
    MenuComponent,
    GameBoardComponent,
    MainComponent,
    SignInComponent,
    RegisterComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    MatDialogModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
