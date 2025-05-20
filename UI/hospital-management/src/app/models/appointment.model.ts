export enum AppointmentStatus {
    Scheduled = 0,
    Completed = 1,
    Canceled = 2
  }
  
  export interface Appointment {
    appointmentId?: number;
    patientId: number;
    doctorId: number;
    appointmentDate: string;
    status: number;
    reason: string;
    createdDate?: string;
  }
  