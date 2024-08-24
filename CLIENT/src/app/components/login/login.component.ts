import { Component, inject, output} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../../_services/account.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  accountService = inject(AccountService);
  cancelBtn = output<boolean>();
  router = inject(Router);
  model: any = {};

  login(){
    this.accountService.login(this.model).subscribe({
      //What to do next
      next: response => {
        console.log(response);
        this.accountService.login(this.model);
        this.router.navigateByUrl('/home');
      },
      //What to do if there is an error
      error: error => {
        console.log(error);
      },
      //What to do when the observable completes
      complete: () => {
        console.log('complete');
      }
    })
  }

  logout(){
    this.accountService.logout();
  }

  cancel() {
    this.cancelBtn.emit(false);
  }
}
