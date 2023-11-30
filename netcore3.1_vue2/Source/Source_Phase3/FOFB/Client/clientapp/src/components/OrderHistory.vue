<template>
<div class="v-card">
    <div class="v-card__title font-weight-small white--text"
            style="border-radius: 5px;
                    font-size: 17px;
                    margin-bottom: 10px;
                    background-color: #00bcd4 !important">
        <strong>履歴</strong>
    </div>
    <div style="padding-top: 10px;" v-if="lstOrderHistory !== null && lstOrderHistory.length > 0">
        <div v-for="(orderhistory, index) in lstOrderHistory" :key="index">
        <!-- for first row -->
            <div v-if="index === 0">
                <!-- Check if display multi line when order many product(diffrent size) -->
                <div v-if="orderhistory.histType === HIST_TYPE_CHANGE_STATUS &&
                                                orderhistory.updatedStatus === RSV_STATUS_ORDER">
                    <!-- Check is last item -->
                    <b-row style="padding-bottom: 10px">
                        <!-- set display dot ・ cloumn -->
                        <b-col xl="1" lg="1" md="1"
                            class="fix-padding"
                            style="padding-left: 80px;
                            margin-top: -4px;
                            font-size: 20px;
                            color: #00bcd4;">・</b-col>   
                            <!-- set display datetime cloumn -->
                        <b-col xl="3" style="max-width: 180px;" lg="3" md="3" class="fix-padding">{{
                            moment(
                            lstOrderHistory[index].updateDtTm
                            ).format("YYYY-MM-DD HH:mm")
                        }}
                        </b-col>
                        <!-- set none display text history cloumn -->
                        <b-col xl="7" lg="7" md="7" class="fix-padding" v-if="
                            orderhistory.histType === HIST_TYPE_CHANGE_QUANTITY||
                            orderhistory.histType === HIST_TYPE_CHANGE_SIZE ||
                            orderhistory.histType === HIST_TYPE_CHANGE_COLOR||
                            orderhistory.histType === HIST_TYPE_CHANGE_SIZE_AND_QUANTITY ||
                            orderhistory.histType === HIST_TYPE_CHANGE_COLOR_AND_QUANTITY ||
                            orderhistory.histType === HIST_TYPE_CHANGE_SIZE_AND_COLOR ||
                            orderhistory.histType === HIST_TYPE_CHANGE_SIZE_AND_COLOR_AND_QUANTITY ||
                            orderhistory.histType === HIST_TYPE_CHANGE_RECIVE_INFO ||
                            orderhistory.histType === HIST_TYPE_CHANGE_SHOP ||
                            orderhistory.histType ===  HIST_TYPE_DELETE_ORDER ">
                            <b-row style="margin-left:unset;margin-top:1px;">
                                本部が下記の内容を変更しました。
                            </b-row>
                            <b-row style="margin-left:unset">
                            <b-col xl="1" lg="1" md="1"  style="padding-right: unset;">
                                ・
                            </b-col>
                            <b-col  style="padding-left: unset;">
                                {{ setDisplayStrHist(
                                    orderhistory.histType,
                                    orderhistory )
                                }}
                                </b-col>
                            </b-row>
                        </b-col>
                        <b-col xl="7" lg="8" md="8" class="fix-padding" v-else>
                            {{ setDisplayStrHist(
                            orderhistory.histType,
                            orderhistory)
                            }}
                        </b-col>
                    </b-row>
                </div>
                <div v-else>
                    <b-row style="padding-bottom: 10px">
                        <!-- set display dot ・ cloumn -->
                        <b-col
                        xl="1"
                        lg="1"
                        md="1"
                        class="fix-padding"
                        style="
                            padding-left: 80px;
                            margin-top: -4px;
                            font-size: 20px;
                            color: #00bcd4;
                        ">・</b-col>
                        <!-- set display datetime cloumn -->
                        <b-col xl="3" style="max-width: 180px;" lg="3" md="3" class="fix-padding">{{
                        moment(
                            lstOrderHistory[index].updateDtTm
                        ).format("YYYY-MM-DD HH:mm")
                        }}
                        </b-col>
                        <!-- set none display text history cloumn -->
                        <b-col xl="7" lg="7" md="7" class="fix-padding" v-if="
                        orderhistory.histType === HIST_TYPE_CHANGE_QUANTITY||
                        orderhistory.histType === HIST_TYPE_CHANGE_SIZE ||
                        orderhistory.histType === HIST_TYPE_CHANGE_COLOR||
                        orderhistory.histType === HIST_TYPE_CHANGE_SIZE_AND_QUANTITY ||
                        orderhistory.histType === HIST_TYPE_CHANGE_COLOR_AND_QUANTITY ||
                        orderhistory.histType === HIST_TYPE_CHANGE_SIZE_AND_COLOR ||
                        orderhistory.histType === HIST_TYPE_CHANGE_SIZE_AND_COLOR_AND_QUANTITY ||
                        orderhistory.histType === HIST_TYPE_CHANGE_RECIVE_INFO ||
                        orderhistory.histType === HIST_TYPE_CHANGE_SHOP ||
                        orderhistory.histType ===  HIST_TYPE_DELETE_ORDER ">
                        <b-row style="margin-left:unset;margin-top:1px;">
                            本部が下記の内容を変更しました。
                        </b-row>
                        <b-row style="margin-left:unset">
                            <b-col xl="1" lg="1" md="1"  style="padding-right: unset;">
                            ・
                            </b-col>
                            <b-col  style="padding-left: unset;">
                            {{ setDisplayStrHist(
                                orderhistory.histType,
                                orderhistory )
                            }}
                            </b-col>
                        </b-row>
                        </b-col>
                        <b-col xl="7" lg="8" md="8" class="fix-padding" v-else>
                        {{ setDisplayStrHist(
                            orderhistory.histType,
                            orderhistory)
                        }}
                        </b-col>
                    </b-row>
                </div>                     
            </div>
            <div v-else>
                <!-- Check if display multi line when order many product(diffrent size) -->
                <div v-if="orderhistory.histType === HIST_TYPE_CHANGE_STATUS &&
                                                        orderhistory.updatedStatus === RSV_STATUS_ORDER">
                        <!-- Check is last item -->
                        <b-row style="padding-bottom: 10px">
                        <!-- set display dot ・ cloumn -->
                            <b-col xl="1" lg="1" md="1"
                                class="fix-padding"
                                style="
                                padding-left: 80px;
                                margin-top: -4px;
                                font-size: 20px;
                                color: #00bcd4;
                                "
                                v-if="
                                moment(
                                    lstOrderHistory[index].updateDtTm
                                ).format('YYYY-MM-DD HH:mm') !==
                                    moment(
                                    lstOrderHistory[index - 1].updateDtTm
                                    ).format('YYYY-MM-DD HH:mm')
                                "
                                >・</b-col>
                            <!-- set none display dot ・ cloumn -->
                            <b-col xl="1" lg="1" md="1" class="fix-padding" style="padding-left: 80px; margin-top: -4px; font-size: 20px; color: #00bcd4;" v-else >・</b-col>
                            <!-- set display datetime cloumn -->
                            <b-col  xl="3" style="max-width: 180px;"  lg="3"  md="3" class="fix-padding"
                                v-if="
                                    moment(
                                    lstOrderHistory[index].updateDtTm
                                    ).format('YYYY-MM-DD HH:mm') !==
                                    moment(
                                        lstOrderHistory[index - 1]
                                        .updateDtTm
                                    ).format('YYYY-MM-DD HH:mm')
                                "
                            >{{
                                moment(
                                lstOrderHistory[index].updateDtTm
                                ).format("YYYY-MM-DD HH:mm")
                            }}
                            </b-col>
                            <!-- set none display datetime cloumn -->
                            <b-col xl="3" style="max-width: 180px;" lg="3" md="3" class="fix-padding" v-else>
                                {{moment(
                                    lstOrderHistory[index - 1]
                                    .updateDtTm
                                ).format('YYYY-MM-DD HH:mm')}}
                            </b-col>
                            <!-- set none display text history cloumn -->
                            <b-col xl="7" lg="7" md="7" class="fix-padding">
                                {{
                                setDisplayStrHist(
                                orderhistory.histType,
                                orderhistory
                                )
                            }}
                            </b-col>
                        </b-row>
                </div>
                <div v-else>
                    <b-row style="padding-bottom: 10px">
                    <!-- set display dot ・ cloumn -->
                    <b-col xl="1" lg="1" md="1"
                        class="fix-padding"
                        style="
                            padding-left: 80px;
                            margin-top: -4px;
                            font-size: 20px;
                            color: #00bcd4;
                        "
                        v-if="
                            moment(
                            lstOrderHistory[index].updateDtTm
                            ).format('YYYY-MM-DD HH:mm') !==
                            moment(
                                lstOrderHistory[index - 1].updateDtTm
                            ).format('YYYY-MM-DD HH:mm')
                        "
                        >・</b-col>
                    <!-- set none display dot ・ cloumn -->
                    <b-col xl="1" lg="1" md="1" class="fix-padding" style="padding-left: 80px; margin-top: -4px; font-size: 20px; color: #00bcd4;" v-else ></b-col>
                    <!-- set display datetime cloumn -->
                    <b-col  xl="3" style="max-width: 180px;"  lg="3"  md="3"
                        class="fix-padding"
                        v-if="
                        moment(
                            lstOrderHistory[index].updateDtTm
                        ).format('YYYY-MM-DD HH:mm') !==
                            moment(
                            lstOrderHistory[index - 1]
                                .updateDtTm
                            ).format('YYYY-MM-DD HH:mm')
                        "
                        >{{
                        moment(
                            lstOrderHistory[index].updateDtTm
                        ).format("YYYY-MM-DD HH:mm")
                        }}
                    </b-col>
                    <!-- set none display datetime cloumn -->
                    <b-col xl="3" style="max-width: 180px;" lg="3" md="3" class="fix-padding" v-else> </b-col>
                    <!-- set none display text history cloumn -->
                    <b-col xl="7" lg="7" md="7" class="fix-padding" v-if="
                        (orderhistory.histType === HIST_TYPE_CHANGE_QUANTITY||
                        orderhistory.histType === HIST_TYPE_CHANGE_SIZE ||
                        orderhistory.histType === HIST_TYPE_CHANGE_COLOR||
                        orderhistory.histType === HIST_TYPE_CHANGE_SIZE_AND_QUANTITY ||
                        orderhistory.histType === HIST_TYPE_CHANGE_COLOR_AND_QUANTITY ||
                        orderhistory.histType === HIST_TYPE_CHANGE_SIZE_AND_COLOR ||
                        orderhistory.histType === HIST_TYPE_CHANGE_SIZE_AND_COLOR_AND_QUANTITY ||
                        orderhistory.histType === HIST_TYPE_CHANGE_RECIVE_INFO ||
                        orderhistory.histType === HIST_TYPE_CHANGE_SHOP ||
                        orderhistory.histType === HIST_TYPE_DELETE_ORDER) &&
                    ((lstOrderHistory[index - 1].histType !== HIST_TYPE_CHANGE_QUANTITY&&
                        lstOrderHistory[index - 1].histType !== HIST_TYPE_CHANGE_SIZE &&
                        lstOrderHistory[index - 1].histType !== HIST_TYPE_CHANGE_COLOR &&
                        lstOrderHistory[index - 1].histType !== HIST_TYPE_CHANGE_SIZE_AND_QUANTITY &&
                        lstOrderHistory[index - 1].histType !== HIST_TYPE_CHANGE_COLOR_AND_QUANTITY &&
                        lstOrderHistory[index - 1].histType !== HIST_TYPE_CHANGE_SIZE_AND_COLOR &&
                        lstOrderHistory[index - 1].histType !== HIST_TYPE_CHANGE_SIZE_AND_COLOR_AND_QUANTITY &&
                        lstOrderHistory[index - 1].histType !== HIST_TYPE_CHANGE_RECIVE_INFO &&
                        lstOrderHistory[index - 1].histType !== HIST_TYPE_CHANGE_SHOP &&
                        lstOrderHistory[index - 1].histType !== HIST_TYPE_DELETE_ORDER)
                    || moment(
                            lstOrderHistory[index].updateDtTm
                        ).format('YYYY-MM-DD HH:mm') !==
                            moment(
                            lstOrderHistory[index - 1]
                                .updateDtTm
                            ).format('YYYY-MM-DD HH:mm'))">
                        <b-row style="margin-left:unset;margin-top:1px;">
                            本部が下記の内容を変更しました。
                        </b-row>
                        <b-row style="margin-left:unset">
                        <b-col xl="1" lg="1" md="1" style="padding-right: unset;">
                            ・
                        </b-col>
                        <b-col style="padding-left: unset;">
                            {{ setDisplayStrHist(
                                orderhistory.histType,
                                orderhistory )
                            }}
                        </b-col>
                        </b-row>
                    </b-col>
                    <!-- set none display text history cloumn -->
                    <b-col xl="7" lg="7" md="7" class="fix-padding" v-else>
                        <b-row style="margin-left:unset" v-if=" (orderhistory.histType === HIST_TYPE_CHANGE_QUANTITY||
                        orderhistory.histType === HIST_TYPE_CHANGE_SIZE ||
                        orderhistory.histType === HIST_TYPE_CHANGE_COLOR||
                        orderhistory.histType === HIST_TYPE_CHANGE_SIZE_AND_QUANTITY ||
                        orderhistory.histType === HIST_TYPE_CHANGE_COLOR_AND_QUANTITY ||
                        orderhistory.histType === HIST_TYPE_CHANGE_SIZE_AND_COLOR ||
                        orderhistory.histType === HIST_TYPE_CHANGE_SIZE_AND_COLOR_AND_QUANTITY ||
                        orderhistory.histType === HIST_TYPE_CHANGE_RECIVE_INFO ||
                        orderhistory.histType === HIST_TYPE_CHANGE_SHOP ||
                        orderhistory.histType ===  HIST_TYPE_DELETE_ORDER)">
                        <b-col xl="1" lg="1" md="1" class="fix-padding" style="padding-right: unset;">
                            ・
                        </b-col>
                        <b-col style="padding-left: unset;">
                            {{ setDisplayStrHist(
                                orderhistory.histType,
                                orderhistory )
                            }}
                        </b-col>
                        </b-row>
                        <b-row v-else>
                        <b-col>
                            {{
                            setDisplayStrHist(
                                orderhistory.histType,
                                orderhistory
                            )
                            }}
                        </b-col>
                        </b-row>
                    </b-col>
                    </b-row>
                </div>
            </div>
        </div>
    </div>
</div>
</template>

<script>
import moment from 'moment'
export default {
  name: 'OrderHistory',
   props: {
    lstOrderHistory: {},
  },
  data: () => ({
      moment: moment,
  }),

}
</script>

<style scoped>
.fix-padding {
  padding-bottom: 6px;
  padding-top: 6px;
}
</style>
