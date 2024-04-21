import { Component } from '@angular/core';
import { AccountsService } from '../accounts.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  model: any = {};
  loggedIn = false;

  constructor(private accountsService : AccountsService){}

  login() {    
    this.accountsService.login(this.model).subscribe({
      next: reponse => {
        console.log(reponse);
        this.loggedIn = true;
      },
      error: error => console.log(error)
    })
  }

  logOut() {
    this.loggedIn = false;
  }

}
