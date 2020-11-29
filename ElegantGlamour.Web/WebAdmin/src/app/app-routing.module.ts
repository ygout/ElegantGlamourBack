import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const authModule = () => import('./modules/auth/auth.module').then(x => x.AuthModule);
const routes: Routes = [
  { path: 'auth', loadChildren: authModule }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
