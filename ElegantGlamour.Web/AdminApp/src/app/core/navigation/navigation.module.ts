import { NavigationComponent } from './navigation.component';
import { SharedModule } from 'src/app/shared/shared.module';
import { NgModule, NO_ERRORS_SCHEMA, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';


@NgModule({
  declarations: [NavigationComponent],
  imports: [
    SharedModule,
    RouterModule,
    FormsModule,
    MDBBootstrapModule.forRoot(),
  ],
  exports: [NavigationComponent]
})
export class NavigationModule { }
