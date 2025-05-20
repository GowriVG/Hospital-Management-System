import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Patient } from '../models/patient.model'; 
import { environment } from '../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class PatientService {

  private apiUrl = '/api/Patients';

  constructor(private http: HttpClient) { }

  createPatient(patient: Patient): Observable<Patient> {
    return this.http.post<Patient>(`${environment.apiUrl}/api/Patients`, patient);
  }

  // Get all patients
  getAllPatients(): Observable<Patient[]> {
    console.log('Fetching all patients from:', `${environment.apiUrl}{this.apiUrl}`);
    return this.http.get<Patient[]>(`${environment.apiUrl}/api/Patients`);
  }

  // Get a patient by ID
  getPatientById(id: number): Observable<Patient> {
    console.log('Fetching patient with ID:', id);
    return this.http.get<Patient>(`${environment.apiUrl}/api/Patients/${id}`);
  }

  // Update a patient
  updatePatient(id: number, patient: Patient): Observable<Patient> {
    return this.http.put<Patient>(`${environment.apiUrl}${this.apiUrl}/${id}`, patient);
  }
  
  // Delete a patient
  deletePatient(id: number): Observable<void> {
    return this.http.delete<void>(`${environment.apiUrl}/api/Patients/${id}`);
  }
}
