<template>
  <div
    class="feeList"
    v-on:keyup.45="onCreateFee"
    v-on:keyup.27="handleBackdropClick"
  >
    <div class="feeList__tab">
      <div
        class="tab_item"
        :class="{ active: index === active }"
        v-for="(item, index) in tabItems"
        :key="index"
        v-on:click="onClick(index)"
      >
        {{ item.text }}
      </div>
    </div>
    <div class="feeList__feature">
      <CheckBox
        content="Hiển thị khoản thu ngừng theo dõi"
        styles="width:250px;"
        :checked="searchParams.isActive"
        :onClick="onShowActiveFee"
      >
      </CheckBox>
      <div class="feature__right">
        <Button
          class="btn"
          content="Thêm mới"
          :onClick="onCreateFee"
          v-on:keyup.13="onCreateFee"
        ></Button>
        <Button
          class="btn-default"
          :onClick="onSearchFee"
          v-on:keyup.13="onSearchFee"
          content="Tìm kiếm"
        ></Button>
        <Button
          class="btn-default"
          :icon="
            require('../../../assets/Resources/ImagesIcons/ic_Remove2.svg')
          "
          :onClick="onMultiDelete"
          v-on:keyup.13="onMultiDelete"
        ></Button>
      </div>
    </div>
    <div class="feeList__table">
      <table>
        <thead>
          <tr class="checked">
            <th class="checkbox">
              <CheckBox
                :style="{
                  width: '120px',
                  marginLeft: '30%',
                  marginTop: '-15%',
                }"
                value="false"
                disabled
              />
            </th>
            <th
              v-for="item in thead"
              v-bind:key="item.className"
              v-bind:class="item.className"
              v-bind:style="item.style"
            >
              <div id="content">{{ item.content }}</div>
              <br />

              <InputVue
                :type="item.className === 'Price' ? 'currency' : 'text'"
                :label="''"
                :onChange="
                  (e) => {
                    if (item.className === 'Price') searchParams.Price = e;
                    if (item.className === 'FeeName') searchParams.FeeName = e;
                  }
                "
                :prefix="
                  require('../../../assets/Resources/ImagesIcons/ic_Filter.svg')
                "
                :style="{ width: '100%', height: '28px;', margin: '0 8px' }"
                v-if="item.hasFilter & (item.className !== 'Period')"
               
              />

              <ComboBox
                :listData="period"
                :style="{
                  marginLeft: '8px',
                  marginRight: '-28px',
                  width: '180px',
                  marginBottom: '12px',
                }"
                :value.sync="searchParams.Period"
                v-if="item.hasFilter & (item.className === 'Period')"
              ></ComboBox>
            </th>
          </tr>
        </thead>

        <tbody class="table_body" v-if="!isLoading">
          <tr
            v-for="(fee, index) in tbody"
            v-bind:key="fee.feeID"
            :class="{ trActive: listDelete[index].isActive }"
          >
            <td>
              <CheckBox
                :style="{
                  width: '40px',
                  marginLeft: '30%',
                  marginTop: '-15%',
                }"
                :checked.sync="listDelete[index]"
                :value="index"
                v-bind:onClick="onAddListDelete"
              />
            </td>

            <td class="FeeName">
              <div class="feeName">
                <div class="feeName__text">{{ fee.feeName }}</div>
                <div class="feeName__tooltip">
                  <Tooltip
                    :icon="
                      require('../../../assets/Resources/ImagesIcons/ic_sprites2.png')
                    "
                    :backgroundPosition="{ x: '-97px', y: '-336px' }"
                    content="Đây là khoản thu mặc định của hệ thống, bạn không thể xóa"
                    v-if="fee.isSystem"
                  />
                </div>
              </div>
            </td>
            <td class="FeeGroup">{{ getGroupName(fee.feeGroupID) }}</td>
            <td class="Price">
              {{
                fee.price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",")
              }}/{{ unit(fee.unit) }}
            </td>
            <td class="Period">{{ Period(fee.period) }}</td>
            <td>{{ paymentDeadline(fee.period) }}</td>
            <td class="IsApplyRemisson">
              <div :class="{ checked: fee.isApplyRemisson }" />
            </td>
            <td class="AllowCreateInvoice">
              <div :class="{ checked: fee.allowCreateInvoice }" />
            </td>
            <td class="AllowCreateReceipt">
              <div :class="{ checked: fee.allowCreateReceipt }" />
            </td>
            <td class="allowRefunds">
              <div :class="{ checked: fee.allowRefunds }" />
            </td>
            <td class="isRequire">
              <div :class="{ checked: fee.isRequire }" />
            </td>
            <td class="isActive"><div :class="{ checked: fee.isActive }" /></td>
            <td>
              <div class="fee__tool">
                <div
                  class="fee__edit"
                  @click="onEdit(fee.feeID)"
                  tabindex="1"
                ></div>
                <div
                  class="fee__duplicate"
                  @click="onDuplicate(fee.feeID)"
                  tabindex="1"
                ></div>
                <div
                  class="fee__delete"
                  @click="onDelete(fee.feeID)"
                  tabindex="1"
                ></div>
              </div>
            </td>
          </tr>
        </tbody>
      </table>
      <div class="loading" v-if="isLoading"></div>
    </div>
    <div class="feeList__footer">
      Tổng số: <b>{{ count }}</b> kết quả
    </div>
    <FeeDetail
      :active.sync="isAdd"
      :title="feeDetailsContent"
      :feeGroup="groups"
      :fee.sync="fee"
      :onSubmit="onSubmit"
      :errorMess="errorMess"
      v-if="isAdd"
    ></FeeDetail>
    <Alert :active.sync="alertActive" :text="alertText"></Alert>
    <Confirm
      :active.sync="confirmActive"
      :text="confirmText"
      :onAccept="onConfirmSubmit"
    ></Confirm>
  </div>
</template>

<script>
import * as api from "../../../js/Api.js";
import Confirm from "../../../components/common/Confirm";
import Alert from "../../../components/common/Alert";
import FeeDetail from "./FeeDetail";
import Tooltip from "../../../components/common/Tooltip";
import feeList from "../../../assets/feeList.JSON";
import period from "../../../assets/period.JSON";
import InputVue from "../../../components/common/InputVue";
import CheckBox from "../../../components/common/CheckBox";
import Button from "../../../components/common/Button";
import ComboBox from "../../../components/common/ComboBox";
export default {
  data() {
    return {
      // khai báo item thanh tab
      tabItems: [
        {
          text: "Quy trình",
        },
        {
          text: "Danh sách khoản thu",
        },
        {
          text: "Đăng ký khoản thu",
        },
        {
          text: "Đăng ký miễn giảm",
        },
      ],

      // tiêu đề bảng
      thead: feeList.thead,

      // nội dung bảng
      tbody: feeList.tbody,

      // hiển thị khoản thu ngừng theo dõi nếu isActive=true và ngược lại
      isActive: true,

      // danh sách kì thu
      period: period,

      // kiểm tra dữ liệu trả về hay chưa , nếu chưa thì hiện loading
      isLoading: true,

      // danh sách khoản thu được chọn
      listDelete: feeList.listDelete,

      // mở dialog thêm mới nếu bằng true
      isAdd: false,
      feeDetailsContent: "",
      // đếm số bản ghi trả về
      count: 0,

      // nhóm khoản thu
      groups: [],
      // khoản thu
      fee: [],

      //danh sách lỗi
      errorMess: {
        feeNameErrMess: "",
        feeGroupErrMess: "",
        priceErrMess: "",
        unitErrMess: "",
        applyObjectErrMess: "",
        periodErrMess: "",
      },
      //chứa thông tin cần tìm kiếm
      searchParams: {
        FeeName: "",
        Price: 0,
        Period: 0,
        isActive: true,
      },
      //type = 0 xóa , type = 1 chỉnh sửa , type = 2 thêm mới ,type =3 xóa nhiều
      type: -1,
      // đóng mở arlert
      alertActive: false,
      // nội dung arlert
      alertText: "",
      //đóng mở confirm
      confirmActive: false,
      // nội dung confirm
      confirmText: "",

      acceptConfirm: false,

      idDelete: 0,
    };
  },
  props: {
    active: {
      type: Number,
      default: 0,
    },
    onClick: {
      type: Function,
      default: () => 1,
    },
  },

  components: {
    CheckBox,
    Button,
    InputVue,
    ComboBox,
    Tooltip,
    FeeDetail,
    Alert,
    Confirm,
  },
  methods: {
    // lấy đơn vị từ mã trả về
    unit(unit) {
      if (unit === 1) return "ngày";
      if (unit === 2) return "tháng";
      if (unit === 3) return "quý";
      if (unit === 4) return "kỳ";
      if (unit === 5) return "năm";
      return "";
    },

    // lấy tên kì từ mã trả về
    Period(period) {
      if (period === 1) return "Tháng";
      if (period === 2) return "Quý";
      if (period === 3) return "Học Kỳ";
      if (period === 4) return "Năm Học";
      return "";
    },

    // lấy tên nhóm khoản thu từ mã
    getGroupName(e) {
      // tìm tên trong danh sách nhóm khoản thu có id=e
      var result = this.groups.find((element) => element.id === e);

      // nếu result = underfined thì trả về trống
      if (typeof result == "undefined") return "";

      return result.text;
    },

    // trả về thời điểm thu
    paymentDeadline(period) {
      if (period === 1) return "Hàng Tháng";
      if (period === 2) return "Hàng Quý";
      if (period === 3) return "Tháng 9";
      if (period === 4) return "Hàng Năm";
      return "";
    },

    // đóng các dialog
    handleBackdropClick() {
      this.isAdd = false;
      this.alertActive = false;
      this.confirmActive = false;
    },
    async onEdit(id) {
      var response = await api.getDataByID("Fees", id);
      this.fee = response.data;
      this.feeDetailsContent = "Chỉnh sửa khoản thu";
      this.type = 1;
      this.isAdd = true;
      this.errorMess = {
        feeNameErrMess: "",
        feeGroupErrMess: "",
        priceErrMess: "",
        unitErrMess: "",
        applyObjectErrMess: "",
        periodErrMess: "",
      };
    },
    onDelete(id) {
      this.confirmText = "bạn có chắc muốn xóa khoản thu này";
      this.confirmActive = true;
      this.idDelete = id;
      this.type = 0;
    },
    async onConfirmSubmit() {
      var fee = this.fee;
      if (this.type === 0) {
        var response = await api.deleteEntity("Fees", this.idDelete);

        if (response.status == 400) {
          this.alertText = response.data.userMsg[0];
          this.alertActive = true;
          this.confirmActive = false;
        } else location.reload();
      } else if (this.type === 1) {
        response = await api.updateEntity("Fees", fee, fee.feeID);

        if (response.status == 400) {
          this.confirmActive = false;
          this.validate(response.data.userMsg);
        } else location.reload();
      } else if (this.type === 2) {
        if (fee.createdDate === "") fee.createdDate;
        delete fee["feeID"];
        fee.applyObject = fee.applyObject.toString();
        response = await api.insertEntity("Fees", fee);

        if (response.status == 400) {
          this.confirmActive = false;
          this.validate(response.data.userMsg);
        } else location.reload();
      } else if (this.type === 3) {
        var flag = true;
        this.listDelete.every((item) => {
          if (item.isSystem !== false && item.isActive === true) {
            this.alertText =
              "Danh sách bạn muốn xóa có khoản thu hệ thống, vui lòng kiểm tra lại";
            this.alertActive = true;
            this.confirmActive = false;
            flag = false;
            return false;
          }
          return true;
        });
        if (flag) {
          this.listDelete.forEach(async (item) => {
            if (item.isActive) {
              response = await api.deleteEntity("Fees", item.id);

              if (response.status == 400) {
                this.alertText = response.data.userMsg[0];
                this.alertActive = true;
                this.confirmActive = false;
              }
              location.reload();
            }
          });
        }
      }
    },

    onDuplicate(id) {
      alert(id + "du");
    },
    onMultiDelete() {
      var flag = false;
      this.listDelete.forEach((item) => {
        if (item.isActive !== false) {
          flag = true;
        }
      });
      if (flag !== true) {
        this.alertText = "Danh sách bạn muốn xóa trống, vui lòng kiểm tra lại";
        this.alertActive = true;
      } else {
        this.confirmText = "bạn có chắc muốn xóa những khoản thu này";
        this.confirmActive = true;
        this.type = 3;
      }
    },
    onAddListDelete(index) {
      this.listDelete[index].isActive = !this.listDelete[index].isActive;
    },
    onCreateFee() {
      this.fee = {
        feeID: -1,
        feeName: "",
        feeGroupID: -1,
        price: 0,
        unit: -1,
        applyObject: "0",
        property: 1,
        period: 1,
        isApplyRemisson: false,
        isRequire: false,
        allowCreateInvoice: false,
        allowCreateReceipt: false,
        isActive: true,
        isInternal: false,
        isSystem: false,
        createdDate: "2021-01-20T21:56:39.807",
        createdBy: null,
        modifiedDate: "2021-01-20T21:56:39.807",
        modifiedBy: null,
      };
      this.errorMess = {
        feeNameErrMess: "",
        feeGroupErrMess: "",
        priceErrMess: "",
        unitErrMess: "",
        applyObjectErrMess: "",
        periodErrMess: "",
      };
      this.feeDetailsContent = "Thêm mới khoản thu";
      this.type = 2;
      this.isAdd = true;
    },
    onSearchFee() {
      this.search();
    },
    onShowActiveFee() {
      if (!this.searchParams.isActive) {
        this.searchParams.isActive = !this.searchParams.isActive;
        this.search();
        
      } else {
         this.searchParams.isActive = !this.searchParams.isActive;
        this.search();
       
      }
    },
    search() {
      var params = "Fees/search?";
      var flag = true;
      if (!this.searchParams.isActive) {
        params = params + "isActive=true&";
        
      }
      if (this.searchParams.FeeName != "") {
        params = params + "FeeName=" + this.searchParams.FeeName + "&";
        flag = false;
      }

      if (this.searchParams.Price != 0) {
        params = params + "Price=" + this.searchParams.Price + "&";
        flag = false;
      }
      if (this.searchParams.Period != 0) {
        params = params + "Period=" + this.searchParams.Period + "&";
        flag = false;
      }
   
      if (flag&this.searchParams.isActive) this.getFees("Fees");
      else this.getFees(params);
    },
    validate(data) {
      console.log(data);
      if (data.indexOf("Tên khoản thu bị trống, vui lòng kiểm tra lại") !== -1)
        this.errorMess.feeNameErrMess =
          "Tên khoản thu bị trống, vui lòng kiểm tra lại";
      if (data.indexOf("Tên khoản thu bị trùng, vui lòng kiểm tra lại") !== -1)
        this.errorMess.feeNameErrMess =
          "Tên khoản thu bị trùng, vui lòng kiểm tra lại";

      if (
        data.indexOf("Mức thu  bị trống hoặc <0 vui lòng kiểm tra lại") !== -1
      )
        this.errorMess.priceErrMess =
          "Mức thu  bị trống hoặc <=0 vui lòng kiểm tra lại";
      if (
        data.indexOf(
          "Mã Đơn vị thu bị trống hoặc <=0, vui lòng kiểm tra lại"
        ) !== -1
      )
        this.errorMess.unitErrMess = "đơn vị thu không được để trống";
    },
    onSubmit(e) {
      this.fee = e;
      if (this.type === 1) {
        this.confirmText = "bạn có chắc muốn sửa khoản thu này";
        this.confirmActive = true;

        this.type = 1;
      } else {
        this.fee = e;
        if (this.type === 2) {
          this.confirmText = "bạn có chắc muốn thêm khoản thu này";
          this.confirmActive = true;

          this.type = 2;
        }
      }
    },
    async getFees(name) {
      var response = await api.getData(name);

      this.tbody = response.data;
      var list = [];

      response.data.forEach((item) => {
        var element = { isActive: false, id: "", isSystem: false };
        element.id = item.feeID;
        element.isSystem = item.isSystem;
        list.push(element);
      });
      this.count = list.length;
      this.listDelete = list;
    },
  },
  async created() {
    await this.getFees("Fees");
    var response = await api.getData("fee-groups");
    var listGroup = [{ id: 0, text: "" }];
    response.data.forEach((item) => {
      var group = { id: "", text: "" };
      group.id = item.feeGroupID;
      group.text = item.feeGroupName;
      listGroup.push(group);
      this.isLoading = false;
    });
    this.groups = listGroup;
  },
};
</script>

<style></style>
