import { Component, EventEmitter, Input, Output } from '@angular/core';
import { User } from '../models/user';
import { AccountsService } from '../accounts.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent {
  model: any = {};
  @Output() cancelRegister = new EventEmitter();

  constructor(private accountsService: AccountsService) {}

  register() {
    this.accountsService.register(this.model).subscribe({
      next: () => {
        this.cancel();
      },
      error: (error) => console.log(error),
    });
  }
  cancel() {
    this.cancelRegister.emit(false);
  }
}
