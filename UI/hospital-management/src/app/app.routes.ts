import { Routes } from '@angular/router';
import { PatientListComponent } from './components/patient/patient-list/patient-list.component';
import { PatientFormComponent } from './components/patient/patient-form/patient-form.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { DoctorListComponent } from './components/doctor/doctor-list/doctor-list.component';
import { DoctorFormComponent } from './components/doctor/doctor-form/doctor-form.component';
import { AppointmentListComponent } from './components/appointment/appointment-list/appointment-list.component';
import { AppointmentFormComponent } from './components/appointment/appointment-form/appointment-form.component';
import { MedicalListComponent } from './components/medicalrecord/medical-list/medical-list.component';
import { MedicalFormComponent } from './components/medicalrecord/medical-form/medical-form.component';
export const routes: Routes = [
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full'
  },
  {
    path: 'dashboard',
    component: DashboardComponent
  },
  {
    path: 'patients',
    component: PatientListComponent,
  },
  {
    path: 'patient/add',     
    component: PatientFormComponent
  },
  {
    path: 'patient/edit/:id',
    component: PatientFormComponent
  },
  {
    path: 'doctors',
    component: DoctorListComponent
  },
  {
    path: 'doctor/add',
    component: DoctorFormComponent
  },
  {
    path: 'doctor/edit/:id',
    component: DoctorFormComponent
  },
  {
    path: 'appointments',
    component: AppointmentListComponent
  },
  {
    path: 'appointments/add',
    component: AppointmentFormComponent
  },
  {
    path: 'appointments/edit/:id',
    component: AppointmentFormComponent
  },
  {
    path:'medicalrecords',
    component: MedicalListComponent
  },
  {
    path:'medical-records/add',
    component: MedicalFormComponent
  }
];