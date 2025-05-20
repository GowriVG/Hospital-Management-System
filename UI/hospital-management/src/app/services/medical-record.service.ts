import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';

export interface MedicalRecord {
  recordId?: number;
  patientId: number;
  doctorId: number;
  diagnosis: string;
  prescription?: string;
  treatmentDate: string;
  createdDate?: string;
}

@Injectable({
  providedIn: 'root'
})
export class MedicalRecordService {

  private apiUrl = '/api/MedicalRecord';

  constructor(private http: HttpClient) { }

  getAllMedicalRecords(): Observable<MedicalRecord[]> {
    return this.http.get<MedicalRecord[]>(`${environment.apiUrl}/api/MedicalRecord`);
  }

  addMedicalRecord(record: MedicalRecord): Observable<any> {
    return this.http.post(`${environment.apiUrl}/api/MedicalRecord`, record);
  }
  
  deleteMedicalRecord(id: number): Observable<any> {
    return this.http.delete(`${environment.apiUrl}/api/MedicalRecord/${id}`,{ observe: 'response' });
  }
}
