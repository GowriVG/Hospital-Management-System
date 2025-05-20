export interface Patient {
    patientId: number;
    firstName: string;
    lastName: string;
    dateOfBirth: Date;
    age: number;
    gender: string;
    address?: string;
    email?: string;
    phoneNumber: string;
    createdDate: Date;
    isDeleted: boolean;
  }
  