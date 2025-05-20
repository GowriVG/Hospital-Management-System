import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
//import { Appointment } from '../models/appointment.model';

// export enum AppointmentStatus {
//   Scheduled = 'Scheduled',
//   Completed = 'Completed',
//   Canceled = 'Canceled'
// }

export interface Appointment {
  appointmentId?: number;
  patientId: number;
  doctorId: number;
  appointmentDate: string;
  //status: AppointmentStatus;
  status: number;
  reason: string;
}

@Injectable({
  providedIn: 'root'
})
export class AppointmentService {
  private apiUrl = '/api/Appointment'; // Update to your backend base URL

  constructor(private http: HttpClient) { }

  createAppointment(appointment: Appointment): Observable<Appointment> {
    return this.http.post<Appointment>(`${environment.apiUrl}/api/Appointment/add`, appointment);
  }

  getAppointmentsByPatient(patientId: number): Observable<Appointment[]> {
    return this.http.get<Appointment[]>(`${environment.apiUrl}/api/patient/${patientId}`);
  }

  getAppointmentsByDoctor(doctorId: number): Observable<Appointment[]> {
    return this.http.get<Appointment[]>(`${environment.apiUrl}/api/doctor/${doctorId}`);
  }

  getAppointmentById(id: number): Observable<Appointment> {
    return this.http.get<Appointment>(`${environment.apiUrl}/api/Appointment/${id}`);
  }

  getAllAppointments(): Observable<Appointment[]> {
    return this.http.get<Appointment[]>(`${environment.apiUrl}/api/Appointment`);  
  }

  updateAppointment(id: number, appointment: Appointment): Observable<Appointment> {
    return this.http.put<Appointment>(`${environment.apiUrl}/api/Appointment/update/${id}`, appointment);
  }

  deleteAppointment(id: number): Observable<void> {
    return this.http.delete<void>(`${environment.apiUrl}/api/Appointment/delete/${id}`);
  }
}
