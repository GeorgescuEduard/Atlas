import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { UserService } from 'src/app/shared/user.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Compiler } from '@angular/core';
import { ProjectModel } from 'src/app/shared/project-model.model';

@Component({
  selector: 'app-view',
  templateUrl: './project-view.component.html',
  styles: []
})
export class ProjectViewComponent implements OnInit {

  formData: ProjectModel;
  language: string;

  constructor(
    public service: UserService,
    private route: ActivatedRoute,
    private _compiler: Compiler

  ) { }

  ngOnInit() {
    console.log('Component initialized');
    this.route.data.subscribe((data: { file: any }) => {
      this.formData = data.file as ProjectModel;
      //this.service.revision();
    });
  }

  editProjectRedirect() {
    window.location.href = this.service.local + 'edit-project/' + this.formData.ProjectKEY;
}

}