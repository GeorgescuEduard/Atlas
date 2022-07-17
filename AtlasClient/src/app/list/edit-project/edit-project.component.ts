import { IssuesModel } from '../../shared/issues-model.model';
import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/shared/user.service';
import { Router, ActivatedRoute } from '@angular/router';
import hljs from 'highlight.js';
import { ProjectModel } from 'src/app/shared/project-model.model';

@Component({
  selector: 'app-edit-project',
  templateUrl: './edit-project.component.html',
  styles: []
})
export class EditProjectComponent implements OnInit {
  config: any;
  formData: ProjectModel;

  constructor(
    public service: UserService,
    private http: HttpClient,
    private route: ActivatedRoute,
  
  ) {}

  ngOnInit() {
    this.route.data.subscribe(
      (data: { file: any }) => {
      this.formData = data.file as ProjectModel;
      this.service.projectData = this.formData;
      //this.service.revision();
    });
    
   /* this.service.getVersionCount().subscribe(
      res => {
          this.service.VersionTag = 'Version ' + (res + 1).toString();
      }
  );*/
  }

}