import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/shared/user.service';


@Component({
  selector: 'app-new-project',
  templateUrl: './new-project.component.html',
  styles: []
})
export class NewProjectComponent implements OnInit {

  constructor(
    public service: UserService,
  ) {}

  ngOnInit() {
    this.service.getUserId();
    this.service.resetProjectForm();
    $("#sidebarCollapse").click(function(e) {
      e.preventDefault();
      $("#wrapper").toggleClass("toggled");
    });
  }
}