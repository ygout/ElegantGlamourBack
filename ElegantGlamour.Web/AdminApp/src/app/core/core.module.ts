import { NgModule, Optional, SkipSelf } from "@angular/core";
import { AdminModule } from "../admin/admin.module";
import { AuthModule } from "../auth/auth.module";
import { NavigationModule } from './navigation/navigation.module';


@NgModule({
  declarations: [],
  imports: [AdminModule, AuthModule, NavigationModule],
  exports: [NavigationModule],
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    if (parentModule) {
      throw new Error("CoreModule is already loaded ! ");
    }
  }
}
