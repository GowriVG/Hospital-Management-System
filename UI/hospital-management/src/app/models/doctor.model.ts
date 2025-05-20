export interface Doctor {
    doctorId: number;
    firstName: string;
    lastName: string;
    specialization: string;
    phoneNumber: string;
    email: string;
  }
  
  export interface AddDoctorDto {
    firstName: string;
    lastName: string;
    specialization: string;
    phoneNumber: string;
    email: string;
    createdDate: Date;
  }
  
  export interface UpdateDoctorDto {
    firstName: string;
    lastName: string;
    specialization: string;
    phoneNumber: string;
    email: string;
  }
  