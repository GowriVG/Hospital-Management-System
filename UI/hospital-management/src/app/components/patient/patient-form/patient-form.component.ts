import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { PatientService } from '../../../services/patient.service';
import { Patient } from '../../../models/patient.model';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-patient-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  providers: [DatePipe],
  templateUrl: './patient-form.component.html',
  styleUrls: ['./patient-form.component.css']
})
export class PatientFormComponent implements OnInit {
  patientForm: FormGroup;
  patientId: number | null = null;

  constructor(
    private fb: FormBuilder,
    private patientService: PatientService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private datePipe: DatePipe
  ) {
    this.patientForm = this.fb.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      dateOfBirth: ['', [Validators.required]],
      gender: ['', [Validators.required]],
      phoneNumber: ['', [Validators.required]],
      email: ['', [Validators.email]],
      address: ['', [Validators.required]],
    });
  }

  ngOnInit(): void {
    this.patientId = this.activatedRoute.snapshot.params['id'];
    if (this.patientId) {
      this.loadPatientData();
    }
  }

  // Load patient data for editing and patch the form
  loadPatientData(): void {
    this.patientService.getPatientById(this.patientId!).subscribe({
      next: (patient: Patient) => {
        // Format the DateOfBirth before patching the form
        const formattedDob = this.datePipe.transform(patient.dateOfBirth, 'yyyy-MM-dd'); 

        this.patientForm.patchValue({
          firstName: patient.firstName,
          lastName: patient.lastName,
          dateOfBirth: formattedDob,
          gender: patient.gender,
          phoneNumber: patient.phoneNumber,
          email: patient.email,
          address: patient.address,
        });
      },
      error: (err) => {
        console.error('Error loading patient data', err);
        alert('Unable to load patient data');
      }
    });
  }  

  // Submit form for adding or updating a patient
  submitForm(): void {
    console.log('Form submitted');
    if (this.patientForm.invalid) {
      this.patientForm.markAllAsTouched();
      console.log('Form is invalid');   

      Object.keys(this.patientForm.controls).forEach(key => {
        const control = this.patientForm.get(key);
        if (control && control.invalid) {
          console.warn(`❌ Invalid field: ${key}`, control.errors);
        }
      });
      alert('Please fill in all required fields correctly.');
      return;
    }

    const patientData: Patient = this.patientForm.value;

    if (this.patientId) {
      console.log('Updating patient:', this.patientId, patientData);
      this.patientService.updatePatient(this.patientId, patientData).subscribe({
        next: () => {
          console.log('Update successful');
          this.router.navigate(['/patients']);
        },
        error: (err) => {
          console.error('Error updating patient:', err);
        }
      });
    } else {
      console.log('Creating patient:', patientData);
      this.patientService.createPatient(patientData).subscribe({
        next: () => {
          console.log('Creation successful');
          this.router.navigate(['/patients']);
        },
        error: (err) => {
          console.error('Error creating patient:', err);
        }
      });
    }
    
  }
  goBack(): void {
    this.router.navigate(['/patients']);
  }
}
