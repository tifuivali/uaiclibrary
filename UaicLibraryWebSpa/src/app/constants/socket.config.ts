import { SocketIoConfig } from 'ng2-socket-io';
import { appSettings } from './application-settings';
export const config: SocketIoConfig = { url: appSettings.socketServiceUrl, options: {} }

