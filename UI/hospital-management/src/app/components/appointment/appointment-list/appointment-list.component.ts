import { Component, OnInit } from '@angular/core';
import { AppointmentService } from '../../../services/appointment.service';
import { Appointment } from '../../../models/appointment.model';
import {NavbarComponent} from '../../navbar/navbar.component';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { AppointmentStatus } from '../../../models/appointment.model';

@Component({
  selector: 'app-appointment-list',
  standalone: true,
  imports: [CommonModule,NavbarComponent,RouterModule],
  templateUrl: './appointment-list.component.html',
  styleUrls: ['./appointment-list.component.css']
})
export class AppointmentListComponent implements OnInit {
  appointments: Appointment[] = [];

  statusMap: { [key: string]: string } = {
    'Scheduled': 'Scheduled',
    'Completed': 'Completed',
    'Canceled': 'Canceled'
  };
  

  constructor(
    private appointmentService: AppointmentService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadAppointments();
  }

  loadAppointments() {
    this.appointmentService.getAllAppointments().subscribe({
      next: (data: Appointment[]) => {
        this.appointments = data.map(app => ({
          ...app,
          //status: this.convertStatus(app.status as unknown as number)

        }));
      },
      error: (err) => {
        console.error('Error loading appointments:', err);
      }
    });
  }
  
  convertStatus(status: number): AppointmentStatus {
    switch(status) {
      case 0: return AppointmentStatus.Scheduled;
      case 1: return AppointmentStatus.Completed;
      case 2: return AppointmentStatus.Canceled;
      default: return AppointmentStatus.Scheduled;
    }
  }

  editAppointment(app: Appointment): void {
    this.router.navigate(['/appointments/edit', app.appointmentId]);
  }

  goToAddAppointment(): void {

    this.router.navigate(['/appointments/add']);
  }

  deleteAppointment(id: number): void {
    if (confirm('Are you sure you want to delete this appointment?')) {
      this.appointmentService.deleteAppointment(id).subscribe({
        next: () => {
          alert('Appointment Deleted.');
          this.loadAppointments();
        },
        error: (err) => console.error('Error deleting appointment:', err)
      });
    }
  }
  goToHome(): void {
    this.router.navigate(['/dashboard']);
  }
}
