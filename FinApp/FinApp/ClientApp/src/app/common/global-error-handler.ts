import { ErrorHandler, Injectable, Injector } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';

import { ErrorService } from '../_services/error.service';
import { NotificationService } from '../_services/notification.service';

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {

    constructor(private injector: Injector) { }

    handleError(error: Error | HttpErrorResponse) {
        const errorService = this.injector.get(ErrorService);
        const notifier = this.injector.get(NotificationService);

        if (error instanceof HttpErrorResponse) {
            // Server Error
            const message = errorService.getServerMessage(error);
            notifier.errorMsg(message);
        } else {
            // Client Error
            const message = errorService.getClientMessage(error);
            notifier.errorMsg(message);
        }
        // Logger should be here
    }
}
