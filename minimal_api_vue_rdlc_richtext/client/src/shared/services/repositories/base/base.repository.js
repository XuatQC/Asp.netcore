import RestClient from './rest-client';

export default class BaseRepository {
  client;

  constructor(servicePath) {
    this.client = new RestClient(servicePath);
  }
}
