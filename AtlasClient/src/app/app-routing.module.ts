import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserComponent } from './user/user.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { LoginComponent } from './user/login/login.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './auth/auth.guard';
import { ViewComponent } from './list/view/view.component';
import { EditComponent } from './list/edit/edit.component';
import { VersionViewComponent } from './versions/version-view/version-view.component';
import { ViewSelectedVersionComponent } from './versions/view-selected-version/view-selected-version.component';
import { NewComponent } from './list/new/new.component';
import { UserService } from './shared/user.service';
import { FileResolver } from './shared/resolvers/issues.service';
import { AccountComponent } from './list/account/account.component';
import { NewProjectComponent } from './list/new-project/new-project.component';
import { ProjectListComponent } from './list/project-list/project-list.component';
import { ProjectViewComponent } from './list/project-view/project-view.component';
import { ProjectsResolver } from './shared/resolvers/projects.service';
import { EditProjectComponent } from './list/edit-project/edit-project.component';
import { VersionListResolver } from './shared/resolvers/version-list.service';
import { SelectedVersionResolver } from './shared/resolvers/selected-version.service';


export const routes: Routes = [
  { path: '', redirectTo: '/user/login', pathMatch: 'full' },
  {
    path: 'user', component: UserComponent,
    children: [
      { path: 'registration', component: RegistrationComponent },
      { path: 'login', component: LoginComponent }
    ]
  },
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'account', component: AccountComponent, canActivate: [AuthGuard] },
  { path: 'view/:file', component: ViewComponent, resolve: {file: FileResolver}, canActivate: [AuthGuard] },
  { path: 'edit/:file', component: EditComponent, resolve: {file: FileResolver}, canActivate: [AuthGuard] },
  { path: 'new', component: NewComponent, canActivate: [AuthGuard] },
  { path: 'new-project', component: NewProjectComponent, canActivate: [AuthGuard] },
  { path: 'project-list', component: ProjectListComponent, canActivate: [AuthGuard] },
  { path: 'project-view/:file', component: ProjectViewComponent, resolve: {file: ProjectsResolver}, canActivate: [AuthGuard] },
  { path: 'edit-project/:file', component: EditProjectComponent, resolve: {file: ProjectsResolver}, canActivate: [AuthGuard] },
  { path: 'version-view/:version', component: VersionViewComponent, resolve: {version: VersionListResolver}, canActivate: [AuthGuard] },
  { path: 'view-selected-version/:file', component: ViewSelectedVersionComponent, resolve: {file: SelectedVersionResolver}, canActivate: [AuthGuard] }
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
