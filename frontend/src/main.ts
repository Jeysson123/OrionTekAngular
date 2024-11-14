import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { provideHttpClient } from '@angular/common/http';  // Import HttpClient provider

// Configure application
bootstrapApplication(AppComponent, {
  providers: [
    provideHttpClient(),  // Provide HttpClient service
  ]
}).catch((err) => console.error('Application bootstrap failed:', err));
