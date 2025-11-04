import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
/*import { FormBuilder, FormGroup, Validators } from '@angular/forms';*/
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService, User } from '../../services/user.service';
import { LeaveFormService, LeaveForm } from '../../services/leaveform.service';

// Angular Material imports
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';



@Component({
  selector: 'app-leave-form',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatButtonModule
  ],
  templateUrl: './leaveform.component.html',
  styleUrls: ['./leaveform.component.css']
 
})
export class LeaveFormComponent implements OnInit {
  leaveForm!: FormGroup;
  users: User[] = [];

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private leaveService: LeaveFormService
  ) {  
  }

  ngOnInit(): void {
    this.leaveForm = this.fb.group({
      applicantId: ['', Validators.required],
      managerId: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      returnDate: ['', Validators.required],
      numberOfDays: ['', [Validators.required, Validators.min(1)]],
      generalComments: ['', Validators.maxLength(500)],
      leaveType: ['', Validators.required]
    });

    this.loadUsers();

    this.leaveForm.get('startDate')?.valueChanges.subscribe(() => this.calculateDays());
    this.leaveForm.get('endDate')?.valueChanges.subscribe(() => this.calculateDays());
  }

  loadUsers() {
    this.userService.getActiveUsers().subscribe({
      next: (data) => this.users = data,
      error: (err) => alert('Error loading users: ' + err.message)
    });
  }

  calculateDays() {
    const start = new Date(this.leaveForm.value.startDate);
    const end = new Date(this.leaveForm.value.endDate);
    if (start && end && end > start) {
      const diffTime = Math.abs(end.getTime() - start.getTime());
      const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24)) + 1;
      this.leaveForm.patchValue({ numberOfDays: diffDays }, { emitEvent: false });
    }
  }

  submit() {
    if (this.leaveForm.invalid) {
      alert('Please fill all required fields.');
      return;
    }

    if (this.leaveForm.value.applicantId === this.leaveForm.value.managerId) {
      alert('Applicant and Manager cannot be the same.');
      return;
    }
    if (new Date(this.leaveForm.value.startDate) < new Date()) {
      alert('Start date cannot be in the past.');
      return;
    }
    if (new Date(this.leaveForm.value.endDate) <= new Date(this.leaveForm.value.startDate)) {
      alert('End date must be after start date.');
      return;
    }
    if (new Date(this.leaveForm.value.returnDate) <= new Date(this.leaveForm.value.endDate)) {
      alert('Return date must be after end date.');
      return;
    }

    const leaveData: LeaveForm = this.leaveForm.value;

    this.leaveService.submitLeave(leaveData).subscribe({
      next: (res) => alert('Leave submitted successfully!'),
      error: (err) => alert('Error: ' + err.error)
    });
  }
}
