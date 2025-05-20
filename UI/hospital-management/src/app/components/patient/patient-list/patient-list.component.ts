import { Component, OnInit } from '@angular/core';
import { PatientService } from '../../../services/patient.service';  
import {NavbarComponent} from '../../navbar/navbar.component';
import { Patient } from '../../../models/patient.model';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-patient-list',
  standalone: true,
  imports: [NavbarComponent,CommonModule,RouterModule],
  templateUrl: './patient-list.component.html',
  styleUrls: ['./patient-list.component.css']
})
export class PatientListComponent implements OnInit  {
  patients: Patient[] = [];

  constructor(private patientService: PatientService, private router: Router) { }

  ngOnInit(): void {
    this.loadPatients();
  }

  loadPatients(): void {
    this.patientService.getAllPatients().subscribe({
      next: (data: Patient[]) => {
        console.log('Patients loaded:', data);
        this.patients = data;
      },
      error: (err) => {
        console.error('Error loading patients:', err);
      }
    });
  }

  // Delete a patient
  deletePatient(id: number): void {
    if (confirm('Are you sure to cancel this Patient?')){
      this.patientService.deletePatient(id).subscribe(() => {
        this.patients = this.patients.filter(patient => patient.patientId !== id);
      });
    }
  }

  // Edit patient (navigate to the form)
  editPatient(id: number): void {
    this.router.navigate(['/patient/edit', id]);
  }
  
  goToAddPatient(): void {
    this.router.navigate(['/patient/add']);
  }

  goToHome(): void {
    this.router.navigate(['/dashboard']);
  }
}
