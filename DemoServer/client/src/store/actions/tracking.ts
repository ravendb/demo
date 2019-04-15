import * as actionTypes from "./actionTypes";
import { DemoThunkAction } from ".";
import { DemoThunkDispatch } from "..";
import { CookieJar } from "../../utils/cookies/CookieJar";
import { gtmInit, emitPageViewEvent } from "../../libs/gtm";
import { createAbsoluteUrl } from "../../utils/paths";

interface SaveGtmContainerId {
    type: actionTypes.TRACKING_SAVE_GTM_CONTAINER_ID;
    googleTagManagerContainerId: string;
}

interface ShowMonit {
    type: actionTypes.TRACKING_SHOW_MONIT;
}

interface WithdrawConsent {
    type: actionTypes.TRACKING_WITHDRAW_CONSENT;
}

interface EnableTracking {
    type: actionTypes.TRACKING_ENABLE;
}

export type TrackingAction = SaveGtmContainerId | ShowMonit | WithdrawConsent | EnableTracking;

function saveGtmContainerId(googleTagManagerContainerId: string): SaveGtmContainerId {
    return {
        type: "TRACKING_SAVE_GTM_CONTAINER_ID",
        googleTagManagerContainerId
    };
}

function showMonit(): ShowMonit {
    return {
        type: "TRACKING_SHOW_MONIT"
    };
}

function withdrawConsent(): WithdrawConsent {
    return {
        type: "TRACKING_WITHDRAW_CONSENT"
    };
}

function enableTracking(): EnableTracking {
    return {
        type: "TRACKING_ENABLE"
    };
}

export function initTrackingData(gtmContainerId: string): DemoThunkAction {
    return (dispatch: DemoThunkDispatch) => {
        
        if (!gtmContainerId) {
            return;
        }

        dispatch(saveGtmContainerId(gtmContainerId));

        const consentStatus = CookieJar.getConsentStatus();

        switch (consentStatus) {
            case "no info":
                dispatch(showMonit());
                return;

            case "given":
                dispatch(startTracking(gtmContainerId));
                return;
        }
    };
}

export function giveTrackingConsent(): DemoThunkAction {
    return (dispatch: DemoThunkDispatch, getState) => {
        const state = getState();
        const { tracking } = state;
        const gtmId = tracking.googleTagManagerContainerId;

        CookieJar.acceptAll();
        dispatch(startTracking(gtmId));
    };
}

export function withdrawTrackingConsent(): DemoThunkAction {
    return (dispatch: DemoThunkDispatch) => {
        CookieJar.acceptWithTrackingDisabled();
        dispatch(withdrawConsent());
    };
}

export function startTracking(googleTagManagerContainerId: string): DemoThunkAction {
    return (dispatch: DemoThunkDispatch) => {
        if (!googleTagManagerContainerId) {
            return;
        }

        gtmInit(googleTagManagerContainerId);

        dispatch(enableTracking());
        dispatch(trackCurrentLocationPageView());
    };
}

export function trackCurrentLocationPageView(): DemoThunkAction {
    return (dispatch: DemoThunkDispatch) => {
        const url = window.location.href;
        dispatch(trackPageView(url));
    };
}

export function trackRelativeUrlPageView(relativeUrl: string): DemoThunkAction {
    return (dispatch: DemoThunkDispatch, getState) => {
        const absoluteUrl = createAbsoluteUrl(relativeUrl);
        dispatch(trackPageView(absoluteUrl));
    };
}

function trackPageView(url: string): DemoThunkAction {
    return (dispatch: DemoThunkDispatch, getState) => {
        const { tracking } = getState();

        if (!tracking.enabled) {
            return;
        }

        emitPageViewEvent(url);
    };
}
