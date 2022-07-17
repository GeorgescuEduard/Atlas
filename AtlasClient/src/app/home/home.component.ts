import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/shared/user.service';
import { Router } from '@angular/router';
import * as jwt_decode from 'jwt-decode';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styles: []
})
export class HomeComponent implements OnInit {
  
  constructor(
    public service: UserService,
    private router: Router
    ) { }

  ngOnInit() {
    this.service.getloggedUserName();
    this.service.getloggedFirstName();
    this.service.getloggedLastName();
    this.service.resetForm();
  }

}
