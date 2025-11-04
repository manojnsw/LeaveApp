
import { Component, signal } from '@angular/core';
/*import { RouterOutlet } from '@angular/router';*/
import { CommonModule } from '@angular/common';
import { LeaveFormComponent } from './components/leaveform/leaveform.component';

@Component({
  selector: 'app-root',
  standalone: true, 
  imports: [/*RouterOutlet,*/CommonModule,       
    LeaveFormComponent],

  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  //title = 'Leave Application';
}
