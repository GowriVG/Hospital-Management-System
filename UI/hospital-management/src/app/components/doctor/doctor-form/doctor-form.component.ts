import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { DoctorService } from '../../../services/doctor.service';
import { AddDoctorDto, Doctor } from '../../../models/doctor.model';

@Component({
  selector: 'app-doctor-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './doctor-form.component.html',
  styleUrls: ['./doctor-form.component.css']
})
export class DoctorFormComponent implements OnInit {
  doctorForm: FormGroup;
  doctorId: number | null = null;

  constructor(
    private fb: FormBuilder,
    private doctorService: DoctorService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {
    this.doctorForm = this.fb.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      specialization: ['', [Validators.required]],
      phoneNumber: ['', [Validators.required, Validators.pattern('^[0-9]+$')]],
      email: ['', [Validators.required, Validators.email]]
    });
  }

  ngOnInit(): void {
    this.doctorId = this.activatedRoute.snapshot.params['id'];
    if (this.doctorId) {
      this.loadDoctorData();
    }
  }

  // Load doctor data for editing and patch the form
  loadDoctorData(): void {
    this.doctorService.getDoctorById(this.doctorId!).subscribe({
      next: (doctor: Doctor) => {
        this.doctorForm.patchValue({
          firstName: doctor.firstName,
          lastName: doctor.lastName,
          specialization: doctor.specialization,
          phoneNumber: doctor.phoneNumber,
          email: doctor.email
        });
      },
      error: (err) => {
        console.error('Error loading doctor data', err);
        alert('Unable to load doctor data');
      }
    });
  }

  // Submit form for adding or updating a doctor
  submitForm(): void {
    console.log('Form submitted');
    if (this.doctorForm.invalid) {
      this.doctorForm.markAllAsTouched();
      console.log('Form is invalid');

      Object.keys(this.doctorForm.controls).forEach(key => {
        const control = this.doctorForm.get(key);
        if (control && control.invalid) {
          console.warn(`❌ Invalid field: ${key}`, control.errors);
        }
      });

      alert('Please fill in all required fields correctly.');
      return;
    }

    const doctorData: Doctor = this.doctorForm.value;

    if (this.doctorId) {
      console.log('Updating doctor:', this.doctorId, doctorData);
      this.doctorService.updateDoctor(this.doctorId, doctorData).subscribe({
        next: () => {
          console.log('Update successful');
          this.router.navigate(['/doctors']);
        },
        error: (err) => {
          console.error('Error updating doctor:', err);
          alert('Error updating doctor.');
        }
      });
    } else {
      console.log('Creating doctor:', doctorData);
      const addDoctorDto: AddDoctorDto = {
        ...doctorData,
        createdDate: new Date()
      };
      this.doctorService.addDoctor(addDoctorDto).subscribe({
        next: () => {
          console.log('Creation successful');
          this.router.navigate(['/doctors']);
        },
        error: (err) => {
          console.error('Error creating doctor:', err);
          alert('Error creating doctor.');
        }
      });
    }
  }
  goBack(): void {
    this.router.navigate(['/doctors']);
  }
}
