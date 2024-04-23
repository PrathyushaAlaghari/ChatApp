import { Component, OnInit } from '@angular/core';
import { AccountsService } from '../accounts.service';
@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css'],
})
export class NavbarComponent implements OnInit {
  model: any = {};
  loggedIn = false;

  constructor(public accountsService: AccountsService) {}

  ngOnInit(): void {}

  login() {
    this.accountsService.login(this.model).subscribe({
      next: (response) => {
        console.log(response);
        this.loggedIn = true;
      },
      error: (error) => console.log(error),
    });
  }

  logOut() {
    this.accountsService.logout();
    this.loggedIn = false;
  }
}
