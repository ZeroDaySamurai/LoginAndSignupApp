import { NgFor } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavComponent } from './components/nav/nav.component';
import { AccountService } from './_services/account.service';
import { HomeComponent } from "./components/home/home.component";
import { LoginComponent } from "./components/login/login.component";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, NgFor, NavComponent, HomeComponent, LoginComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  private accountService = inject(AccountService);  

  ngOnInit() {
    this.setCurrentUser();
  }

  setCurrentUser() {
    const useString = localStorage.getItem('user');
    if(!useString) return;
    const user = JSON.parse(useString);
    this.accountService.currentUser.set(user);
  }
}
