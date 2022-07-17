import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/shared/user.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-list-controller',
  templateUrl: './list-controller.component.html',
  styles: []
})
export class ListControllerComponent implements OnInit {

  token: string;

  constructor(
    public service: UserService,
    private router: Router
  ) { }

  ngOnInit() {
    this.token = localStorage.getItem('token');
    this.service.getloggedFirstName();
    this.service.getloggedLastName();
    this.service.getloggedRank();
  }

  navigateToNew() {
    this.router.navigate(['/new']);
  }
  navigateToTest() {
    this.router.navigate(['/share']);
  }
  navigateToHome() {
    this.router.navigate(['home']);
  }
  navigateToAccount() {
    this.router.navigate(['account']);
  }
  navigateToNewProject() {
    this.router.navigate(['new-project']);
  }
  navigateToProjectsList() {
    this.router.navigate(['project-list']);
  }
}