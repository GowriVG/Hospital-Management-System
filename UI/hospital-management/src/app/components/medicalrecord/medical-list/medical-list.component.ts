import { Component } from '@angular/core';
import { OnInit } from '@angular/core';
import { MedicalRecordService } from '../../../services/medical-record.service';
import { MedicalRecord } from '../../../services/medical-record.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { NavbarComponent } from '../../navbar/navbar.component';

@Component({
  selector: 'app-medical-list',
  standalone: true,
  imports: [RouterModule, CommonModule,NavbarComponent],
  templateUrl: './medical-list.component.html',
  styleUrl: './medical-list.component.css'
})
export class MedicalListComponent implements OnInit {
  medicalRecords: MedicalRecord[] = [];
  errorMessage = '';

  constructor(private medicalRecordService: MedicalRecordService, private router: Router) { }

  ngOnInit(): void {
    this.loadMedicalRecords();
  }

  loadMedicalRecords(): void {
    this.medicalRecordService.getAllMedicalRecords().subscribe({
      next: records => this.medicalRecords = records,
      error: err => this.errorMessage = 'Failed to load medical records'
    });
  }

  deleteMedicalRecord(id: number): void {
    if (confirm('Are you sure to delete this medical record?')) {
      this.medicalRecordService.deleteMedicalRecord(id).subscribe({
        next: (response) => {
          console.log('Delete response:', response);
          if (response.status === 200 || response.status === 204) {
            this.loadMedicalRecords();  // Refresh the list
            this.errorMessage = '';    // Clear any previous error message
          } else {
            this.errorMessage = 'Failed to delete medical record.';
          }
        },
        error: (err) => {
          console.error('Delete error response:', err);
          this.errorMessage = 'Failed to delete medical record.';
        }
      });
    }
  }

   addMedicalRecord(): void {
    this.router.navigate(['/medical-records/add']);
  }

  goToHome(): void {
    this.router.navigate(['/dashboard']);
  }

}
