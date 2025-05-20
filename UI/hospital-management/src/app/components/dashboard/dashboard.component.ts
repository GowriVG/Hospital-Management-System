import { Component,OnInit  } from '@angular/core';
import { NavbarComponent } from '../navbar/navbar.component';
import { CommonModule } from '@angular/common';
import { Patient } from '../../models/patient.model';
import { DoctorService } from '../../services/doctor.service';
import { PatientService } from '../../services/patient.service';
import { AppointmentService } from '../../services/appointment.service';
import { MedicalRecordService,MedicalRecord } from '../../services/medical-record.service';
import { Appointment } from '../../models/appointment.model';
import { Doctor } from '../../models/doctor.model';
@Component({
  selector: 'app-dashboard',
  imports: [NavbarComponent,CommonModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.css'
})
export class DashboardComponent implements OnInit {
  totalDoctors: number = 0;
  totalPatients: number = 0;
  totalAppointments: number = 0;
  totalMedicalRecords: number = 0;

  constructor(private doctorService: DoctorService,
              private patientService: PatientService, // Assuming you have a service for patients
              private appointmentService: AppointmentService, // Assuming you have a service for appointments
              private medicalRecordService: MedicalRecordService // Assuming you have a service for medical records
  ) {}

  ngOnInit(): void {
    this.loadDoctorCount();
    this.loadPatientCount();
    this.loadAppointmentCount();
    this.loadMedicalRecordCount();
  }

  loadPatientCount(): void {
    this.patientService.getAllPatients().subscribe({
      next: (patients: Patient[]) => {
        this.totalPatients = patients.length;
      },
      error: (err) => {
        console.error('Error fetching patients:', err);
        alert('Failed to load patient count.');
      }
    });
  }

  
  loadDoctorCount(): void {
    this.doctorService.getAllDoctors().subscribe({
      next: (doctors: Doctor[]) => {
        this.totalDoctors = doctors.length;
      },
      error: (err) => {
        console.error('Error fetching doctors:', err);
        alert('Failed to load doctor count.');
      }
    });
  }

  loadAppointmentCount(): void {
    this.appointmentService.getAllAppointments().subscribe({
      next: (appointments: Appointment[]) => {
        this.totalAppointments = appointments.length;
      },
      error: (err) => {
        console.error('Error fetching appointments:', err);
        alert('Failed to load appointment count.');
      }
    });
  }

  loadMedicalRecordCount(): void {
    this.medicalRecordService.getAllMedicalRecords().subscribe({
      next: (records: MedicalRecord[]) => {
        this.totalMedicalRecords = records.length;
      },
      error: (err) => {
        console.error('Error fetching medical records:', err);
        alert('Failed to load medical record count.');
      }
    });
  }
}

