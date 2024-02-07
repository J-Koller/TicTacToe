import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SignInComponent } from '../sign-in/sign-in.component';
import { RegisterComponent } from '../register/register.component';
import { MainComponent } from '../main/main.component';

const routes: Routes = [
  {path: 'signin', component: SignInComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'main', component: MainComponent},
  {path: '', redirectTo: 'signin', pathMatch: 'full'},
  {path: '**', redirectTo: 'signin', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
