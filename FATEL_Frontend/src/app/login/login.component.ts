import { Component, OnInit } from '@angular/core';
import {Login} from "../entities/DTOs/login";
import {LoginService} from "../services/login.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  username: string = "";
  password: string = "";
  unauthorized: boolean = true;

  hide: boolean = true;

  constructor(private loginService: LoginService, private router: Router) { }

  ngOnInit(): void {
  }

  async login() {
    let loginDto: Login = {
      username: this.username,
      password: this.password
    };
    const token = await this.loginService.login(loginDto);
    if(!token)
    {
      this.unauthorized = token;
    }
    else
    {
      localStorage.setItem('token', token);
      await this.router.navigate(['']);
    }
  }
}
