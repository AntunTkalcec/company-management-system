import { AuthData } from './authData';
import { Company } from './company';
import { UserRequest } from './user-request';

export interface User {
  id: number;
  firstName: string;
  lastName: string;
  userName: string;
  email: string;
  password: string;
  isAdmin: boolean;
  timeOffCount: number;
  salary: number;
  isOnleave: boolean;
  authenticationInfo: AuthData;
  requests: Array<UserRequest>;
  companyId: number;
  company: Company;
}
