import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'ERP',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44390',
    redirectUri: baseUrl,
    clientId: 'ERP_App',
    responseType: 'code',
    scope: 'offline_access ERP',
  },
  apis: {
    default: {
      url: 'https://localhost:44362',
      rootNamespace: 'Jiepei.ERP',
    },
  },
} as Environment;
