import { Component, OnInit } from '@angular/core';
import { Appointment} from '../../../models/appointment.model';
import { AppointmentService } from '../../../services/appointment.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { RouterModule } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-appointment-form',
  standalone: true,
  imports: [CommonModule,ReactiveFormsModule,RouterModule, FormsModule],
  templateUrl: './appointment-form.component.html',
  styleUrls: ['./appointment-form.component.css']
})
export class AppointmentFormComponent implements OnInit {
  appointment! : FormGroup;
  appointmentId?: number;
  

  statusOptions: Array<keyof typeof this.statusMap> = ['Scheduled', 'Completed', 'Cancelled'];

  statusMap = {
    Scheduled: 0,
    Completed: 1,
    Cancelled: 2
  } as const;
  

  reverseStatusMap: { [key: number]: string } = {
    0: 'Scheduled',
    1: 'Completed',
    2: 'Cancelled'
  };

  constructor(
    private fb: FormBuilder,
    private service: AppointmentService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.appointment = this.fb.group({
      patientId: [0, [Validators.required]],
      doctorId: [0, [Validators.required]],
      appointmentDate: ['', [Validators.required]],
      status: ["Scheduled", [Validators.required]],
      reason: ['', [Validators.required]]
    });
  }
  

  ngOnInit(): void {
    this.appointmentId = Number(this.route.snapshot.params['id']);
    console.log('Appointment ID from route:', this.appointmentId);
    if (this.appointmentId) {
      this.loadAppointmentData();
      console.log('Route param id:', this.appointmentId);
    }
  }
  // Load appointment data for editing and patch the form
  loadAppointmentData(): void {
    this.service.getAppointmentById(this.appointmentId!).subscribe({
      next: (appointment) => {
        this.appointment.patchValue({
          patientId: appointment.patientId,
          doctorId: appointment.doctorId,
          appointmentDate: this.formatDateToInput(appointment.appointmentDate),
          //status: this.reverseStatusMap[appointment.status as number], // convert number to string
          //status: appointment.status,
          status: this.reverseStatusMap[appointment.status as number],
          reason: appointment.reason
        });
        console.log('Patched form value:', this.appointment.value); 
      },
      error: (err) => {
        console.error('Error loading appointment:', err);
      }
    });
  }
  
  // Helper method to format date for date input (yyyy-MM-dd)
  formatDateToInput(date: Date | string): string {
    if (!date) return '';

    const d = new Date(date);
    const offset = d.getTimezoneOffset();
    const local = new Date(d.getTime() - offset * 60000);

  return local.toISOString().slice(0, 16); // Gets "yyyy-MM-ddTHH:mm"
  }
  
  // Create or update appointment
  saveAppointment(): void {
    if (this.appointment.valid) {
      const formValue = this.appointment.value;

    const appointmentData: Appointment = {
      appointmentId: this.appointmentId, 
      patientId: formValue.patientId,
      doctorId: formValue.doctorId,
      appointmentDate: new Date(formValue.appointmentDate).toISOString(),
      //appointmentDate: formValue.appointmentDate,
      //status: this.statusMap[formValue.status as keyof typeof this.statusMap],
      status: formValue.status,
      reason: formValue.reason
    };
    console.log('Submitting appointment:', appointmentData); // 🔍 Debug log


      if (this.appointmentId) {
        this.service.updateAppointment(this.appointmentId, appointmentData).subscribe({
          next: () => {
            alert('Appointment updated successfully');
            this.router.navigate(['/appointments']);
          },
          error: (err) => {
            console.error('Error updating appointment:', err);
          }
        });
      } else {
        this.service.createAppointment(appointmentData).subscribe({
          next: () => {
            alert('Appointment created successfully');
            this.router.navigate(['/appointments']);
          },
          error: (err) => {
            console.error('Error creating appointment:', err);
          }
        });
      }
    } 
    else {
      alert('Please fill in all required fields.');
    }
  }
  // Cancel appointment
  cancelAppointment(): void {
    if (this.appointmentId) {
      this.service.deleteAppointment(this.appointmentId).subscribe({
        next: () => {
          alert('Appointment canceled successfully');
          this.router.navigate(['/appointments']);
        },
        error: (err) => {
          console.error('Error canceling appointment:', err);
        }
      });
    } else {
      alert('No appointment to cancel');
    }
  }
  // Go back to appointment list
  goBack(): void {
    this.router.navigate(['/appointments']);
  }
}
