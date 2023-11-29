import RepositoryFactory from 'repositories/base/repository.factory';

export default class BaseService {
  serviceName;

  dao;

  constructor(serviceName) {
    this.serviceName = serviceName;
    this.dao = RepositoryFactory.getRepository(serviceName);
  }
}
