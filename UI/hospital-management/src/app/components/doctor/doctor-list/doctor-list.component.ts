import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { Doctor } from '../../../models/doctor.model';
import { DoctorService } from '../../../services/doctor.service';
import { NavbarComponent } from '../../navbar/navbar.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-doctor-list',
  standalone: true,
  imports: [NavbarComponent,CommonModule,RouterModule],
  templateUrl: './doctor-list.component.html',
  styleUrls: ['./doctor-list.component.css']
})
export class DoctorListComponent implements OnInit {
  doctors: Doctor[] = [];

  constructor(private doctorService: DoctorService, private router: Router) {}

  ngOnInit(): void {
    this.loadDoctors();
  }

  loadDoctors(): void {
    this.doctorService.getAllDoctors().subscribe((data) => {
      this.doctors = data;
    });
  }

  deleteDoctor(id: number): void {
    if (confirm('Are you sure to delete this doctor?')) {
      this.doctorService.deleteDoctor(id).subscribe(() => {
        this.loadDoctors();
      });
    }
  }

  editDoctor(id: number): void {
    this.router.navigate(['/doctor/edit', id]);
  }

  addDoctor(): void {
    this.router.navigate(['/doctor/add']);
  }

  goToHome(): void {
    this.router.navigate(['/dashboard']);
  }
}
