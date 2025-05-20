import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MedicalRecordService, MedicalRecord } from '../../../services/medical-record.service';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-medical-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule,RouterModule],
  templateUrl: './medical-form.component.html',
  styleUrls: ['./medical-form.component.css']
})
export class MedicalFormComponent {

  medicalForm: FormGroup;
  successMessage = '';
  errorMessage = '';

  constructor(
    private fb: FormBuilder,
    private medicalRecordService: MedicalRecordService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {
    this.medicalForm = this.fb.group({
      patientId: [null, [Validators.required]],
      doctorId: [null, [Validators.required]],
      diagnosis: ['', [Validators.required]],
      prescription: ['', [Validators.required]],
      treatmentDate: [new Date().toISOString().split('T')[0], [Validators.required]],
    });
  }

  onSubmit(): void {
    this.successMessage = '';
    this.errorMessage = '';

    if (this.medicalForm.invalid) {
      this.medicalForm.markAllAsTouched();
      this.errorMessage = 'Please fill in all required fields correctly.';
      return;
    }

    const medicalRecordData: MedicalRecord = this.medicalForm.value;

    this.medicalRecordService.addMedicalRecord(medicalRecordData).subscribe({
      next: (res) => {
        console.log('Success Response:', res);
        this.successMessage = 'Medical record added successfully!';
        this.router.navigate(['/medicalrecords']);
      },
      error: err => {
        console.error('Error Response:', err);
    
        // ⚠️ TEMP FIX: Force navigate if you know record was added (only for testing!)
        if (err.status === 200 || err.status === 201 || err.status === 0) {
          console.warn('Forcing success despite error due to empty/malformed response');
          this.router.navigate(['/medicalrecords']);
        } else {
          this.errorMessage = 'Failed to add medical record';
        }
      }
    });
    
  }

  goBack(): void {
    this.router.navigate(['/medicalrecords']);
  }

  resetForm(): void {
    this.medicalForm.reset({
      patientId: null,
      doctorId: null,
      diagnosis: '',
      prescription: '',
      treatmentDate: new Date().toISOString().split('T')[0]
    });
  }
}
