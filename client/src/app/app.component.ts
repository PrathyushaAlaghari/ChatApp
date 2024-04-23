import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AccountsService } from './accounts.service';
import { User } from './models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  users: any;
  // model : any = {};
  // loggedIn = false;

  constructor(
    private http: HttpClient,
    private accountsService: AccountsService
  ) {}

  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser() {
    const userString = localStorage.getItem('user');
    console.log('userString', userString);
    if (!userString) return;
    const user: User = JSON.parse(userString);
    this.accountsService.setCurrentUser(user);
  }

  // login() {
  //   this.accountsService.login(this.model).subscribe({
  //     next: reponse => {
  //       console.log(reponse);
  //       this.loggedIn = true;
  //     },
  //     error: error => console.log(error)
  //   })
  // }
}
