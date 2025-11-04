import { bootstrapApplication } from '@angular/platform-browser';
/*import { appConfig } from './app/app.config';
import { AppModule } from './app/app.module';

bootstrapApplication(AppModule, appConfig)
  .catch((err) => console.error(err));*/
  import { AppComponent } from './app/app.component';
  import { provideHttpClient } from '@angular/common/http';

bootstrapApplication(AppComponent, {
  providers: [
    provideHttpClient()  
  ]
})
  .catch(err => console.error(err));

