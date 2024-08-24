import { Component, inject, OnInit } from '@angular/core';
import { LoginComponent } from "../login/login.component";
import { RegisterComponent } from "../register/register.component";
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [LoginComponent, RegisterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  http = inject(HttpClient);
  registerMode = false;
  users: any;

  ngOnInit() {
    this.getUsers();
  }

  registerToggle() {
    this.registerMode = !this.registerMode;
  } 

  cancelMode(event: boolean) {
    this.registerMode = event;
  }

  getUsers(){
    this.http.get('https://localhost:5001/api/users').subscribe({
      next: response => this.users = response,
      error: error => console.error(error),
      complete: () => console.log('HTTP request completed.')
    });
  }
}
