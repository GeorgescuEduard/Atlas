import { Component, OnInit } from '@angular/core';
import { IssuesModel } from 'src/app/shared/issues-model.model';
import { ProjectModel } from 'src/app/shared/project-model.model';
import { UserService } from 'src/app/shared/user.service';


@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  styles: []
})
export class ProjectListComponent implements OnInit {
  config: any;
  formData: ProjectModel;

  constructor(
    public service: UserService,
  ) {}

  ngOnInit() {
    this.service.getUserId();
    this.service.resetProjectForm();
    this.service.getProjectsList();
    $("#sidebarCollapse").click(function(e) {
      e.preventDefault();
      $("#wrapper").toggleClass("toggled");
    });
  }

  pageChanged(event) {
    this.service.config.currentPage = event;
    this.service.curentPage = event;
    this.service.getPaginationCount();
    this.service.refreshList();
  }

  projectView(file: ProjectModel) {
    this.formData = Object.assign({}, file);
    window.location.href = this.service.local + 'project-view/' + this.formData.ProjectKEY;
  }

}