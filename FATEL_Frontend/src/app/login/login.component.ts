import { Component, OnInit } from '@angular/core';
import {Login} from "../entities/DTOs/login";
import {LoginService} from "../services/login.service";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  username: string = "";
  password: string = "";

  constructor(private loginService: LoginService) { }

  ngOnInit(): void {
  }

  async login() {
    let loginDto: Login = {
      username: this.username,
      password: this.password
    };
    const token = await this.loginService.login(loginDto);
    localStorage.setItem('token', token);
  }

  logout(){
    localStorage.removeItem('token');

  }
}
