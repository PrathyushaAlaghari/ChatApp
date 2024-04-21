import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AccountsService } from './accounts.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  users: any;
  // model : any = {};
  // loggedIn = false;

  constructor(private http: HttpClient, private accountsService: AccountsService){}

  ngOnInit(): void {
    this.http.get('https://localhost:5001/api/user').subscribe(
      {
        next : response => this.users = response,
        error : error => console.log(error),
        complete : () => console.log("http request is  complete")
      }
    )
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

