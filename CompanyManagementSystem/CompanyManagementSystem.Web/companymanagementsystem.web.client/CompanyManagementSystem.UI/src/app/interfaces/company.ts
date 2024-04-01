import { User } from './user';

export interface Company {
  id: number;
  name: string;
  companyImage: string;
  pto: number;
  staff: Array<User>;
}
