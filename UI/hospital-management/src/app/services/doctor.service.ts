import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Doctor, AddDoctorDto, UpdateDoctorDto } from '../models/doctor.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DoctorService {
  private apiUrl = '/api/Doctors';

  constructor(private http: HttpClient) {}

  addDoctor(doctor: AddDoctorDto): Observable<Doctor> {
    return this.http.post<Doctor>(`${environment.apiUrl}/api/Doctors`, doctor);
  }

  getAllDoctors(): Observable<Doctor[]> {
    return this.http.get<Doctor[]>(`${environment.apiUrl}/api/Doctors`);
  }

  getDoctorById(id: number): Observable<Doctor> {
    return this.http.get<Doctor>(`${environment.apiUrl}/api/Doctors/${id}`);
  }

  updateDoctor(id: number, doctor: UpdateDoctorDto): Observable<Doctor> {
    return this.http.put<Doctor>(`${environment.apiUrl}${this.apiUrl}/${id}`, doctor);
  }

  deleteDoctor(id: number): Observable<Doctor> {
    return this.http.delete<Doctor>(`${environment.apiUrl}/api/Doctors/${id}`);
  }
}
