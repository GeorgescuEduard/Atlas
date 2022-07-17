import { IssuesModel } from '../../shared/issues-model.model';
import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/shared/user.service';
import { Router, ActivatedRoute } from '@angular/router';
import hljs from 'highlight.js';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styles: []
})
export class EditComponent implements OnInit {
  config: any;
  formData: IssuesModel;

  constructor(
    public service: UserService,
    private http: HttpClient,
    private route: ActivatedRoute,

  ) { }

  ngOnInit() {
    this.route.data.subscribe(
      (data: { file: any }) => {
        this.formData = data.file as IssuesModel;
        this.service.formData = this.formData;
        this.service.revision();
      });

    /* this.service.getVersionCount().subscribe(
       res => {
           this.service.VersionTag = 'Version ' + (res + 1).toString();
       }
   );*/
  }

}